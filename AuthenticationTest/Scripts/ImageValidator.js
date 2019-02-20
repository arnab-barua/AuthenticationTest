function ValidateImageFull(acceptableSize, acceptableWidth, acceptableHeight, element) {
    var _URL = window.URL || window.webkitURL;
    var iconPre = element.siblings('.validation-icon').remove();
    var file = element.prop('files')[0];
    if (file) {
        acceptableSize = acceptableSize * 1024 * 1024;
        var span = $('<span />').addClass('validation-icon');
        var image = new Image();
        image.onload = function () {
            span.addClass('text-danger');
            span.html('<i class="fa fa-times"></i>');
            if (file.size > acceptableSize) {
                span.insertAfter(element);
                alert('Invalid Size');
                element.val(null);
            }
            else if (this.height != acceptableHeight || this.width != acceptableWidth) {
                span.insertAfter(element);
                alert('Invalid dimension');
                element.val(null);
            }
            else {
                span.addClass('text-success').removeClass('text-danger');
                span.html('<i class="fa fa-check"></i>');
                span.insertAfter(element);
            }
        };
        image.src = _URL.createObjectURL(file);
    }
}

function ValidateImageSize(acceptableSize, element) {
    var _URL = window.URL || window.webkitURL;
    var iconPre = element.siblings('.validation-icon').remove();
    var file = element.prop('files')[0];
    if (file) {
        acceptableSize = acceptableSize * 1024 * 1024;
        var span = $('<span />').addClass('validation-icon');
        var image = new Image();
        image.onload = function () {
            span.addClass('text-danger');
            span.html('<i class="fa fa-times"></i>');
            if (file.size > acceptableSize) {
                span.insertAfter(element);
                alert('Invalid Size');
                element.val(null);
            }
            else {
                span.addClass('text-success').removeClass('text-danger');
                span.html('<i class="fa fa-check"></i>');
                span.insertAfter(element);
            }
        };
        image.src = _URL.createObjectURL(file);
    }
}

function ValidateImageDimension(acceptableWidth, acceptableHeight, element) {
    var _URL = window.URL || window.webkitURL;
    var iconPre = element.siblings('.validation-icon').remove();
    var file = element.prop('files')[0];
    if (file) {
        var span = $('<span />').addClass('validation-icon');
        var image = new Image();
        image.onload = function () {
            span.addClass('text-danger');
            span.html('<i class="fa fa-times"></i>');
            if (this.height != acceptableHeight || this.width != acceptableWidth) {
                span.insertAfter(element);
                alert('Invalid dimension');
                element.val(null);
            }
            else {
                span.addClass('text-success').removeClass('text-danger');
                span.html('<i class="fa fa-check"></i>');
                span.insertAfter(element);
            }
        };
        image.src = _URL.createObjectURL(file);
    }
}