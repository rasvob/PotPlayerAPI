(function () {
    function buttonClicked(event) {
        event.preventDefault();
        console.log('submit');
        var action = this.getAttribute('data-pot-action');
        document.querySelector('input[name="PotPlayerAction"]').value = action;

        console.log(document.querySelector('input[name="PotPlayerAction"]').value);

        var handle = Array.from(document.querySelectorAll('input[name="radio-handle"]')).filter(function (item) {
           return item.checked;
        })[0].value;
        document.querySelector('input[name="Handle"]').value = handle;
        console.log(document.querySelector('input[name="Handle"]').value);
        document.querySelector('form[name="pot-form"]').submit();
    }

    function init() {
        var buttons = document.querySelectorAll('.pot-actions button');
        buttons.forEach(function (item) {
            item.addEventListener('click', buttonClicked);
        });

        var radios = document.querySelectorAll('input[name="radio-handle"]');
        var isAnyChecked = Array.from(radios).some(function (item) {
            return item.checked;
        });

        if (!isAnyChecked && radios.length > 0) {
            radios[0].checked = true;
        }
    }

    //MainCode
    console.log('Ready');
    init();
})();