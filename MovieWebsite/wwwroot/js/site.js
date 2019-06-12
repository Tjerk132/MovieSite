function ConfirmRating(MovieId) {
    if (confirm("You can rate each movie only once, are you sure you want to submit this rating?"))
    {
        var Rating = document.getElementById(MovieId).value;
        $.ajax({
            type: 'POST',
            url: "/Rating/SubmitRating",
            data: { Rating, MovieId },
            success: function (Message) {
                document.getElementById("ErrorMessage").innerHTML = Message; 
            },
        });
    }
}
