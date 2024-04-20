
    function highlightStars(starCount) {
        var stars = document.querySelectorAll('.star-wrap input');
    for (var i = 0; i < stars.length; i++) {
            if (i < starCount) {
        stars[i].previousSibling.textContent = 'Chọn ' + (i + 1) + ' sao';
            } else {
        stars[i].previousSibling.textContent = 'Chọn ' + (i + 1) + ' sao';
            }
        }
    }

    var starInputs = document.querySelectorAll('.star-wrap input');
    for (var i = 0; i < starInputs.length; i++) {
        starInputs[i].addEventListener('mouseover', function (event) {
            var currentStarIndex = parseInt(event.target.value);
            highlightStars(currentStarIndex);
        });
    starInputs[i].addEventListener('mouseleave', function() {
        highlightStars(0);
        });
    starInputs[i].addEventListener('click', function(event) {
            var selectedStarIndex = parseInt(event.target.value);
    // Điều khiển hành vi khi chọn sao ở đây
    console.log('Đã chọn ' + selectedStarIndex + ' sao.');
        });
    }

