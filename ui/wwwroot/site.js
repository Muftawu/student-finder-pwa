
function dismissModal(modalId) {
    var modalElement = document.querySelector(modalId);
    if (modalElement) {
        var modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) {
            modalInstance.hide();
        }
    }
}