function getDigits(number, power) {
    if (typeof (power) == 'undefined')
        power = 1;
        
    var n = (number + "").split("");    // make n an array of digits

    // loop through n and get the power of each digit
    for (i = 0; i < n.length; i++) {
        n[i] = Math.pow(n[i], power);
    }

    return n;
}

function sum(numArray) {
    var sumTot = 0;
    for (var i = 0; i < numArray.length; i++) {
        sumTot = sumTot + parseInt(numArray[i]);
    }

    return sumTot;
}
