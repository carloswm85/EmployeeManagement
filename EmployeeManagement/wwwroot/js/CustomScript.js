function confirmDelete(uniqueId, isDeleteButtonClicked) {
    var deleteSpan = document.getElementById('deleteSpan_' + uniqueId);
    var confirmDeleteSpan = document.getElementById('confirmDeleteSpan_' + uniqueId);

    if (isDeleteButtonClicked) {
        deleteSpan.style.display = 'none';
        confirmDeleteSpan.style.display = 'block';
    } else {
        deleteSpan.style.display = 'block';
        confirmDeleteSpan.style.display = 'none';
    }
}
