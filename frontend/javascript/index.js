








var main = (function(){
    
    // 1: 从intergerArray挑选出偶数, 实现至少三种不同写法
    var intergerArray = [1,2,3,4,5,6,7,8,9,10];
    var resArr;
    function filterOddNumber1(){ 
        resArr=[];       
        intergerArray.forEach(x=>{
            if(x%2==0)
             resArr.push(x);
        });
        console.log[resArr];
    }

    function filterOddNumber2(){
        resArr=[];
        for(var i=0;i<intergerArray.length;i++)
        {
            if(i%2!=0)
            {
                resArr.push(intergerArray[i]);
            }
        }
    }

    function filterOddNumber3(){
        resArr=[];
        var i=0,j=0;
        for(i=1,j=0;i<intergerArray.length;i+=2,j++)
        {
            res[j]=intergerArray[i];
        }
    }

    // 2: 下面已经定义好了 Human 的一个类，以及它的行为，请实现Student与Teacher两种不同对象继承于该类
    // Sutdent应当具有 doHomeWork方法
    // Teacher应当具有 checkHomeWork方法
   
    function Human(name){
        this.name = name
        this.myName = function(){
            return "my name is :" + this.name
        }
    }

    function Teacher(name){
        Human.call(this);
        this.name=name;
        this.checkHomeWork = function(){
            return "homework is ... emmm .... ok!"
        }
    }



    function Student(name){    
        this.name=name;
        this.doHomeWork = function(){
            return "i hate homework, but i must do that to be a better me...."
        }
    }

    Student.prototype=new Human();
    var princess = new Student("CoCoLee")
    console.info(princess.doHomeWork())


    var poorKid = new Teacher("KingXu")
    console.info(poorKid.checkHomeWork())
    
    // 如果继承是正常的, 下面的语句应该能够通过
    console.info(princess.myName())
    console.info(poorKid.myName())

    // 3: 请实现一个简单版本的Ajax函数, 只需要实现post即可，兼容IE6，7，8版本
    function ajaxPost(){
        var request;
        if(window.XMLHttpRequest)
        {
            request=new XMLHttpRequest();//IE7+
        }
        else
        {
            request=new ActiveXObject("Microsoft.XMLHTTP");//IE6,IE5
        }

        console.log("request:",request);
        var type ="POST";
        var url ="";
        var isAsync = true;//默认true
        request.open(type,url,isAsync);
        request.setRequestHeader("Content-type","application/x-www-form-urlencoded");
        request.send("");
        request.onreadystatechange = function () {
            alert(request.readyState );
            alert(request.status);
            alert(request.responseText);
            
            if (request.readyState == 4 && request.status == 200) {
                console.log(request.responseText);
         };
        }
    }

    // 4: 设置一个随机200-1000ms的延迟函数, 到期之后, 它有4分之1的概率会失败
    // (使用Promise 完成)
   function randomSleep(){
        return new Promise( function(resolve, reject){
            setTimeout(() => {
            
                console.log("Success!");
                resolve("Success!");
            
                // console.log("Failed");
                // reject("Failed");                  
            }
             , Math.ceil(Math.random()*1000) 
            )             
         }
         )
    }

    randomSleep().then(function(rs){
        console.log('good job, i got it!');
    }).catch(function(error) {
        console.log('oh my god, it is failed!');
    });
})()