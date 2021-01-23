// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Wait for the DOM to be ready
$(function () {
    // Initialize form validation on the registration form.
    // It has the name attribute "registration"
    $("form[name='contactForm']").validate({
        errorElement: 'div',
        errorClass: 'errMsg',
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side
            fromName: "required",
            emailBody: "required",
            fromEmail: {
                required: true,
                // Specify that email should be validated
                // by the built-in "email" rule
                email: true
            }
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element.parent());  //* THIS IS WHERE I AM HAVING TROUBLE WITH
        },
        
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            $('#send-text').hide();
            $('#send-loader').show();
            form.submit();
        }
    });
});
