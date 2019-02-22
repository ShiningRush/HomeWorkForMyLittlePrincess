(function (){
    var _downloading = false
    var ieVersion = navigator.userAgent.match(/MSIE (\d+)/)
    ieVersion = ieVersion && ieVersion[1]
    // ie9 以下 || o != null
    if (!!ieVersion && ieVersion < 9) {
        displayTopMessage()
    }
    
    function displayTopMessage(){
        var messageElement = document.createElement('div')
        messageElement.id = 'topMessage'
        messageElement.appendChild(document.createTextNode('您的浏览器版本过低，无法正常使用本系统，请更新你的浏览器版本或下载我们提供的客户端程序。'));
        var appLink = document.createElement('a');
        appLink.appendChild(document.createTextNode('点击下载最新客户端程序'));
        appLink.onclick = function(){
            if(_downloading){
                alert('正在下载中，请不要反复点击，如果没有弹出下载框，请检查您的安全设置然后F5刷新页面再次尝试')
                return
            }
    
            _downloading = true
            location.href = location.protocol + "//" + location.host + '/api/ClientPack/GetClientPackFile?clientIp=' + location.protocol + "//" + location.host
        }
    
        messageElement.appendChild(appLink)
        document.getElementsByTagName('body')[0].insertBefore(messageElement, document.getElementById('app'))
    }
})();