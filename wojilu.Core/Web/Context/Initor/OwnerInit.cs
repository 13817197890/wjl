/*
 * Copyright (c) 2010, www.wojilu.com. All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.Text;
using wojilu.Members.Interface;
using wojilu.Common.AppBase;
using wojilu.Common.Menus.Interface;
using wojilu.Web.Mvc.Routes;
using wojilu.Web.Url;
using wojilu.Members.Users.Domain;
using wojilu.Common;
using wojilu.Members.Groups.Domain;
using wojilu.Web.Mvc;

namespace wojilu.Web.Context.Initor {

    public class OwnerInit : IContextInit {


        public virtual void Init( MvcContext ctx ) {


        }

        private void initEditorUploadPath( MvcContext ctx ) {

            if (ctx.viewer.IsLogin) {
                // 此处使用onwer，避免二级域名下的跨域问题
                ctx.SetItem( "editorUploadUrl", Link.To( ctx.owner.obj, "Users/UserUpload", "UploadForm", -1, -1 ) );
                ctx.SetItem( "editorMyPicsUrl", Link.To( ctx.owner.obj, "Users/UserUpload", "MyPics", -1, -1 ) );
            }
        }


        private bool spaceStopped( MvcContext ctx, IMember owner ) {

            if (owner.GetType() != typeof( User )) return false;
            if (Component.IsEnableUserSpace()) return false;

            // 三种例外
            if (ctx.route.isAdmin) return false;
            if (ctx.route.isInPath( "Microblogs" )) return false;
            if (ctx.url.PathAndQuery.IndexOf( "Layouts/TopNav/Nav" ) >= 0) return false;

            return true;
        }

        private void updateRoute_ByOwnerMenus( MvcContext ctx, IMember owner ) {

            String cleanUrlWithoutOwner = ctx.route.getCleanUrlWithoutOwner( ctx );

            if (isHomePath( cleanUrlWithoutOwner )) {

                if (isCustomHome( owner ) == false) {
                    return;
                }

                List<IMenu> list = InitHelperFactory.GetHelper( ctx ).GetMenus( ctx.owner.obj );
                updateRoute_Menu( ctx, list, "default" );
                ctx.utils.setIsHome( true );

            }
            else {
                List<IMenu> list = InitHelperFactory.GetHelper( ctx ).GetMenus( ctx.owner.obj );
                updateRoute_Menu( ctx, list, cleanUrlWithoutOwner );
            }
        }

        private Boolean isHomePath( String cleanUrlWithoutOwner ) {
            if (cleanUrlWithoutOwner == string.Empty) return true;
            if (strUtil.EqualsIgnoreCase( cleanUrlWithoutOwner, "default" )) return true;
            return false;
        }

        private Boolean isCustomHome( IMember owner ) {

            Boolean homepageCustom = false;

            if (owner.GetType() == typeof( User )) {
                return homepageCustom;
            }

            return true;
        }

        private void updateRoute_Menu( MvcContext ctx, List<IMenu> list, String cleanUrlWithoutOwner ) {
            foreach (IMenu menu in list) {
                if (cleanUrlWithoutOwner.Equals( menu.Url )) { // 如果友好网址相同

                    // 获取实际的网址
                    String fullUrl = UrlConverter.getMenuFullPath( ctx, menu );
                    Route.setRoutePath( fullUrl );

                    Route newRoute = RouteTool.Recognize( fullUrl, ctx.web.PathApplication );
                    if (newRoute == null) break;

                    refreshRouteAndOwner( ctx, newRoute );

                    break;
                }
            }
        }

        private void refreshRouteAndOwner( MvcContext ctx, Route newRoute ) {


            if (newRoute.owner != ctx.route.owner || newRoute.ownerType != ctx.route.ownerType) {
                ctx.utils.setRoute( newRoute );
                Init( ctx ); // 当前Owner已经变换，所以需要重新更新owner
            }
            else {
                ctx.utils.setRoute( newRoute );

            }
        }


    }

}
