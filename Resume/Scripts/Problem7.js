// Problem 7 - find first 5 happy numbers

// square digits of number and replace number with sum of square root
// repeat until number is 1, this is a happy number
// if any calculated number is repeated, not a happy number

function fndHappy() {
    var cnt = 0;
    var number = 1;
    var arrHappy = [];
    var n = 0;
    var chk = [];

    // while number is not 1
    while (n != 1) {
        // on first pass, n will be 0 so set to 1
        if (n === 0) {
            n = number;
        }
        var digits = getDigits(n, 2);      // get computed power for each digit
        var n = sum(digits);                    // sum the computed powers

        if (n != 1) {
            var x = chk.indexOf(n)
            if (x < 0){
                chk.push(n)
            }
            else {
                chk = [];
                number++;
                n = number;
            }
        }
        else {
            arrHappy.push(number);
            cnt++;
            number++;
            n = number;
        }

        if (cnt === 5) {
            document.getElementById('prob7Output').value = arrHappy;
            n = 1;
        }
    }
}

function happy(number) {
    var digits = getDigits(number, 2);
    var digitSum = sum(digits);

    return (digitSum == 1);
}