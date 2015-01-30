// Problem 8 determine if Armstrong Number
function fndAs() {
    var asArr = [];

    for (x = 100; x < 1000; x++) {
        var digits = getDigits(x, 3);  // get total of digits to given power

        var digitSum = sum(digits);

        if (x == digitSum) {
            asArr.push(x);
        }
    }
    document.getElementById('prob8Output').value = asArr;
}