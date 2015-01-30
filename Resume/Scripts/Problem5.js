// Problem 5
function fizzBuzz() {
    var i = 1;
    var x = 0;
    var y = 0;
    var prob5Out = "";

    var text = (1).toString();
    while (i <= 100) {

        x = i;
        x %= 3;
        y = i;
        y %= 5;

        text = i.toString();
        if (y == 0) { text = 'Buzz'; }
        if (x == 0) { text = 'Fizz'; }
        if (x == 0 && y == 0) { text = 'FizzBuzz'; }
        prob5Out = prob5Out + text + ",";

        i++;
    }
    document.getElementById('prob5Output').value = prob5Out;
}