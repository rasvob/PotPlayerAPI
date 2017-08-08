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
    
    function ajaxButtonClicked(event) {
        event.preventDefault();
        var action = this.getAttribute('data-pot-action');
        var handle = Array.from(document.querySelectorAll('input[name="radio-handle"]')).filter(function (item) {
            return item.checked;
        })[0].value;
        postRemoteActionToServer(action, handle);
    }
    
    function postRemoteActionToServer(action, handle) {
        var url = "/pot/AjaxRemote";
        var errorContainer = document.getElementById('error-container');
        errorContainer.innerHTML = '';

        fetch(url, {
            method: 'post',
            body: JSON.stringify({
                PotPlayerAction: action,
                Handle: handle
            }),
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        }).then(function (res) {
            if (!res.ok) {
                if (res.status === 404) {
                    window.location.reload();
                }
                errorContainer.innerHTML = '<div class="alert alert-danger" role="alert"> <strong>Error!</strong> Can\'t execute selected command. </div>';
            }

        }).catch(function (exc) {
            console.log(exc);
            errorContainer.innerHTML = '<div class="alert alert-danger" role="alert"> <strong>Error!</strong> Can\'t execute selected command. </div>';
        });
    }

    function init() {
        var buttons = document.querySelectorAll('.pot-actions button');
        buttons.forEach(function (item) {
            item.addEventListener('click', ajaxButtonClicked);
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