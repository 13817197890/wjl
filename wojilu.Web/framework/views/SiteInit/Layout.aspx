<!DOCTYPE html>
<html lang="zh-CN">
<head>
<title>#{pageTitle}</title>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
<meta content="#{pageKeywords}" name="Keywords" />
<meta content="#{pageDescription}" name="Description" />
<link href="~css/bootstrap/css/bootstrap.css?v=#{cssVersion}" rel="stylesheet" />
<link href="~css/wojilu._base.css?v=#{cssVersion}" rel="stylesheet" />
#{siteSkinContent}
<script>var __funcList = []; var _run = function (aFunc) { __funcList.push(aFunc); }; var require = { urlArgs: 'v=#{jsVersion}' };</script>
<!--[if IE 6]>
<link href="~css/bootstrap/ie6.css?v=#{cssVersion}" rel="stylesheet">
<link href="~css/wojilu.core.ie6.css?v=#{cssVersion}" rel="stylesheet">
<script>_run( function() { if($.browser.msie && $.browser.version=="6.0") wojilu.ui.resetDropMenu(); });</script>
<![endif]-->
</head>

<body>
#{topNav}
<div class="container page-container" id="page-container">
    #{header}
    <div id="page-main-wrap">
    #{layout_content}
    </div>
</div>
<script>
    _run(function () {
        require(['wojilu.core.base'], function (x) { x.customSkin().backTop(); });
    });
</script>
<script data-main="~js/main" src="~js/lib/require-jquery-wojilu.js?v=#{jsVersion}"></script>
#{statsJs}
</body>
</html>
