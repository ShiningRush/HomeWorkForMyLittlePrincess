export function getAge(strBirthday, delimiter) {
    var returnAge;
    var returnMonth;
    var returnDay;
    if (!delimiter) {
        delimiter = "/";
    }
    var strBirthdayArr = strBirthday.split(delimiter);
    var birthYear = strBirthdayArr[0];
    var birthMonth = strBirthdayArr[1];
    var birthDay = strBirthdayArr[2];

    var d = new Date();
    var nowYear = d.getFullYear();
    var nowMonth = d.getMonth() + 1;
    var nowDay = d.getDate();
    //计算年
    if (nowYear == birthYear) {
        returnAge = 0; //同年 则为0岁  
    } else {
        var ageDiff = nowYear - birthYear; //年之差  
        if (ageDiff > 0) {
            if (nowMonth == birthMonth) {
                var dayDiff = nowDay - birthDay; //日之差  
                if (dayDiff < 0) {
                    returnAge = ageDiff - 1;
                } else {
                    returnAge = ageDiff;
                }
            } else {
                var monthDiff = nowMonth - birthMonth; //月之差  
                if (monthDiff < 0) {
                    returnAge = ageDiff - 1;
                } else {
                    returnAge = ageDiff;
                }
            }
        } else {
            returnAge = -1; //返回-1 表示出生日期输入错误 晚于今天  
        }
    }
    //计算月
    if (returnAge == 0) {
        if (nowYear == birthYear) {
            var monthDiff = nowMonth - birthMonth;
        } else {
            var monthDiff = nowMonth - birthMonth + 12;
        }
        returnMonth = monthDiff;
        var dayDiff = nowDay - birthDay;
        if (dayDiff < 0) {
            returnMonth = monthDiff - 1;
        }

        //计算天
        if (returnMonth == 0) {
            if (nowMonth == birthMonth) {
                var dayDiff = nowDay - birthDay;
            } else {
                var dayDiff = nowDay - birthDay + getPreMonth(strBirthday, delimiter);
            }
            if (dayDiff >= 0) {
                returnDay = dayDiff;
            }
            return returnDay + "+D"; //返回天数
        } else {
            return returnMonth + "+M"; //返回月份
        }
    } else {
        return returnAge + "+Y"; //返回周岁年龄  
    }



}

function getPreMonth(date, delimiter) {
    var arr = date.split(delimiter);
    var year = arr[0]; //获取当前日期的年份  
    var month = arr[1]; //获取当前日期的月份  
    var day = arr[2]; //获取当前日期的日  
    var days = new Date(year, month, 0);
    days = days.getDate();
    return days;
}
export function displayAge(date) {

    let birthday = new Date(date).toLocaleDateString();
    let age = getAge(birthday);
    let ageArray = age.split('+');
    if (ageArray[1] == "D") {
        age = ageArray[0] + "天";
    } else if (ageArray[1] == "M") {
        age = ageArray[0] + "月";
    } else if (ageArray[1] == "Y") {
        age = ageArray[0] + "岁";
    }
    return age;

}