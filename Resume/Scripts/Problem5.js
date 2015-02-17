// Problem 5
function fizzBuzz() {
    var i = 1;
    var x = 0;
    var y = 0;
    var prob5Out = "";
    var prob5AOut = "";
    var prob5BOut = "";
    var prob5COut = "";
    var prob5DOut = "";
    var prob5EOut = "";

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
        prob5Out = prob5Out + text;

        if (i == 100){
            prob5EOut = prob5Out;
            prob5Out = "";
        }
        if (i == 80) {
            prob5DOut = prob5Out;
            prob5Out = "";
        }
        if (i == 60) {
            prob5COut = prob5Out;
            prob5Out = "";
        }
        if (i == 40) {
            prob5BOut = prob5Out;
            prob5Out = "";
        }
        if (i == 20) {
            prob5AOut = prob5Out;
            prob5Out = "";
        }
        if ((i % 10) != 0) { prob5Out = prob5Out + ',' }
        i++;
    }
    document.getElementById('prob5AOutput').value = prob5AOut;
    document.getElementById('prob5BOutput').value = prob5BOut;
    document.getElementById('prob5COutput').value = prob5COut;
    document.getElementById('prob5DOutput').value = prob5DOut;
    document.getElementById('prob5EOutput').value = prob5EOut;
}