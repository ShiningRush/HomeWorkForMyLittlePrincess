var main = (function(){
    
    // 1: 从intergerArray挑选出偶数, 实现至少三种不同写法
    var intergerArray = [1,2,3,4,5,6,7,8,9,10]
    function filterOddNumber1(){

    }

    function filterOddNumber2(){

    }

    function filterOddNumber3(){

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

    function Teacher(){
        this.checkHomeWork = function(){
            return "homework is ... emmm .... ok!"
        }
    }

    function Student(){
        this.doHomeWork = function(){
            return "i hate homework, but i must do that to be a better me...."
        }
    }

    var princess = new Student("CoCoLee")
    console.info(princess.doHomeWork())

    var poorKid = new Teacher("KingXu")
    console.info(poorKid.checkHomeWork())

    // 如果继承是正常的, 下面的语句应该能够通过
    // console.info(princess.myName())
    // console.info(poorKid.myName())

    // 3: 请实现一个简单版本的Ajax函数, 只需要实现post即可，兼容IE6，7，8版本
    function ajaxPost(){

    }

    // 4: 设置一个随机200-1000ms的延迟函数, 到期之后, 它有4分之1的概率会失败
    // (使用Promise 完成)
    function randomSleep(){
        
    }

    randomSleep().then(function(rs){
        console.log('good job, i got it!');
    }).catch(function(error) {
        console.log('oh my god, it is failed!');
    });
})()