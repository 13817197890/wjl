/*
 * Copyright (c) 2010, www.wojilu.com. All rights reserved.
 */

using System;

using wojilu.Web.Mvc;
using wojilu.Web.Mvc.Attr;

using wojilu.Members.Users.Domain;

using wojilu.Common.Skins;
using wojilu.Members.Sites.Domain;
using wojilu.Common.Feeds.Service;
using wojilu.Common.Pages.Service;
using wojilu.Members.Users.Interface;
using wojilu.Common.Feeds.Interface;
using wojilu.Common.Pages.Interface;

namespace wojilu.Web.Controller {

    public class SiteInitController : ControllerBase {

        public SiteInitController() {
           
        }

        public virtual void Index() {
          
        }

        public virtual void SelectDb() {
            set( "link", to( Init ) );
        }

        [NonVisit]
        public virtual void InitData() {
            set( "link", to( Init ) );
            set( "selectDbLink", to( SelectDb ) );

            String dbType = db.getDatabaseType();
            if (strUtil.IsNullOrEmpty( dbType )) {
                throw new Exception( "数据库配置错误，请参考官方配置示例。" );
            }

            set( "dbType", db.getDatabaseType() );
        }

        [NonVisit]
        public virtual void Register() {
            
        }

        [NonVisit]
        public virtual void Initok() {

          
        }

        private Boolean hasMember() {
            return User.count() > 0;
        }

        [HttpPost, DbTransaction]
        public virtual void Init() {

           
        }

        private Boolean isInit() {
            return true;
        }


    }
}

