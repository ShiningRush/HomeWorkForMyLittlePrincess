var main = (function(){
    console.info(document.querySelectorAll(".problem.fifth button"))
    var childs=document.querySelectorAll(".problem.fifth button");
   
    childs.forEach(x => {
        x.onclick=function(){
            document.getElementById("result").innerHTML = x.innerHTML;
        };
    });

    var clickcount=0;

    document.querySelector(".problem.sixth .btn.btn-primary").onclick=function()
    {
        clickcount++;
        var div_sixth = document.querySelectorAll(".problem.sixth div")[1];
        if(clickcount==1)
        {div_sixth.style.opacity=0;}
        else if(clickcount==2)
        {
            div_sixth.style.float='left';
            div_sixth.style.width=0;                        
        }
        else clickcount=0;       
    }

    

    document.querySelector(".problem.seventh .btn.btn-primary").onclick=function()
    {
        var ul_seventh=document.querySelector(".problem.seventh ul");
        var li_seventh_length=document.querySelectorAll(".problem.seventh ul li").length;
        var ul_seventh_length=ul_seventh.childNodes.length;
        for(var i=0;i<li_seventh_length;i++)
        {
            ul_seventh.innerHTML+="<li>萝卜</li>";
        }
    }


    
    
})()



