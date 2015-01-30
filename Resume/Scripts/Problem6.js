// Prob 6 Find perfect number
function prfHandler(n) {
    var num = parseInt(n);
    var tmpNum = 0;

    if (num > 0) {
        tmpNum = prfNum(n);
        document.getElementById('prob6Output').value = ((tmpNum === num) ? "True" : "False");
    }
    else {
        var prfArr = [];
        num = 0;
        for (var i = 1; i <= 1000; i++) {
            num += i;
            tmpNum = prfNum(num);

            if (tmpNum === num) {
                prfArr.push(tmpNum);
            }
        }
        document.getElementById('prob62Output').value = prfArr;
    }
}
function prfNum(num) {
    var prfAnswer = "";
    var i = 0;
    var temp = 0;
    n = parseInt(num);

    for (i = 1; i <= n / 2; i++) {
        if (n % i == 0) {
            temp += i;
        }
    }
    return temp;
}