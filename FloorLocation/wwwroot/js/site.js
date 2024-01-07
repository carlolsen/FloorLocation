let canEdit = true;
let originalIdValue;
let originalClearanceValue;
let newIdValue;
let newClearanceValue;

async function postData(url = '', data = {}) {
	const response = await fetch(url, {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json',
		},
		body: JSON.stringify(data),
	});
	return response.json();
}

/*
postData("/api/update", { LocationName: '10B', LocationId: '1', IsClearance: 'Y' }).then((data) => {
	console.log(data);
});
*/

function trash(elem) {
	if (!canEdit) {
		const identity = elem.id;
		const id = identity.slice(0, -5);
		const tdButtons = document.getElementById(id + 'buttons');
		tdButtons.parentElement.style.display = 'none';
		originalIdValue = '';
		originalClearanceValue = '';
		newIdValue = '';
		newClearanceValue = '';
		canEdit = true;
		postData('/api/delete', {
			LocationName: id,
			LocationId: '',
			IsClearance: '',
		}).then((data) => {
			window.location = '/Home/Index?PageSize=5&PageNumber=1';
		});
	}
}
function save(elem) {
	if (!canEdit) {
		const identity = elem.id;
		const id = identity.slice(0, -4);
		const tdButtons = document.getElementById(id + 'buttons');
		const tdId = document.getElementById(id + 'id');
		newIdValue = tdId.firstChild.value;
		console.log(tdId.firstChild.value);
		tdId.innerHTML = newIdValue;
		const tdClearance = document.getElementById(id + 'clearance');
		console.log(tdClearance.firstChild.value);
		newClearanceValue = tdClearance.firstChild.value;
		tdClearance.innerHTML = newClearanceValue;
		tdButtons.innerHTML = '';
		const btnUpdate = document.createElement('span');
		btnUpdate.setAttribute('class', 'btn');
		btnUpdate.setAttribute('onclick', 'update(this)');
		const textUpdate = document.createTextNode('Update');
		btnUpdate.appendChild(textUpdate);
		btnUpdate.setAttribute('id', id + 'update');
		const btnRemove = document.createElement('span');
		btnRemove.setAttribute('class', 'btn');
		btnRemove.setAttribute('onclick', 'remove(this)');
		const textRemove = document.createTextNode('Remove');
		btnRemove.appendChild(textRemove);
		btnRemove.setAttribute('id', id + 'remove');
		const separate = document.createTextNode(' | ');
		tdButtons.appendChild(btnUpdate);
		tdButtons.appendChild(separate);
		tdButtons.appendChild(btnRemove);
		originalIdValue = '';
		originalClearanceValue = '';
		canEdit = true;
		postData('/api/update', {
			LocationName: id,
			LocationId: newIdValue,
			IsClearance: newClearanceValue,
		}).then((data) => {
			console.log(data);
		});
		newIdValue = '';
		newClearanceValue = '';
	}
}
function cancel(elem) {
	if (!canEdit) {
		const identity = elem.id;
		const id = identity.slice(0, -6);
		const tdButtons = document.getElementById(id + 'buttons');
		const tdClearance = document.getElementById(id + 'clearance');
		const textClearance = document.createTextNode(originalClearanceValue);
		tdClearance.innerHTML = '';
		tdClearance.appendChild(textClearance);
		const tdId = document.getElementById(id + 'id');
		const textId = document.createTextNode(originalIdValue);
		tdId.innerHTML = '';
		tdId.appendChild(textId);
		tdButtons.innerHTML = '';
		const btnUpdate = document.createElement('span');
		btnUpdate.setAttribute('class', 'btn');
		btnUpdate.setAttribute('onclick', 'update(this)');
		const textUpdate = document.createTextNode('Update');
		btnUpdate.appendChild(textUpdate);
		btnUpdate.setAttribute('id', id + 'update');
		const btnRemove = document.createElement('span');
		btnRemove.setAttribute('class', 'btn');
		btnRemove.setAttribute('onclick', 'remove(this)');
		const textRemove = document.createTextNode('Remove');
		btnRemove.appendChild(textRemove);
		btnRemove.setAttribute('id', id + 'remove');
		const separate = document.createTextNode(' | ');
		tdButtons.appendChild(btnUpdate);
		tdButtons.appendChild(separate);
		tdButtons.appendChild(btnRemove);
		originalIdValue = '';
		originalClearanceValue = '';
		newIdValue = '';
		newClearanceValue = '';
		canEdit = true;
	}
}
function remove(elem) {
	if (canEdit) {
		canEdit = false;
		const identity = elem.id;
		const id = identity.slice(0, -6);
		const tdButtons = document.getElementById(id + 'buttons');
		const tdClearance = document.getElementById(id + 'clearance');
		originalClearanceValue = tdClearance.textContent.trim();
		const tdId = document.getElementById(id + 'id');
		originalIdValue = tdId.textContent.trim();
		tdButtons.innerHTML = '';
		const btnDelete = document.createElement('span');
		btnDelete.setAttribute('class', 'btn');
		btnDelete.setAttribute('style', 'background-color:#cc0000;color:#ffffff');
		btnDelete.setAttribute('onclick', 'trash(this)');
		const textDelete = document.createTextNode('Delete');
		btnDelete.appendChild(textDelete);
		btnDelete.setAttribute('id', id + 'trash');
		const btnCancel = document.createElement('span');
		btnCancel.setAttribute('class', 'btn');
		btnCancel.setAttribute('style', 'background-color:#000000;color:#ffffff');
		btnCancel.setAttribute('onclick', 'cancel(this)');
		const textCancel = document.createTextNode('Cancel');
		btnCancel.appendChild(textCancel);
		btnCancel.setAttribute('id', id + 'cancel');
		const separate = document.createTextNode(' | ');
		tdButtons.appendChild(btnDelete);
		tdButtons.appendChild(separate);
		tdButtons.appendChild(btnCancel);
	}
}
function update(elem) {
	if (canEdit) {
		canEdit = false;
		const identity = elem.id;
		const id = identity.slice(0, -6);
		const tdButtons = document.getElementById(id + 'buttons');
		const tdClearance = document.getElementById(id + 'clearance');
		originalClearanceValue = tdClearance.textContent.trim();
		const tdId = document.getElementById(id + 'id');
		originalIdValue = tdId.textContent.trim();
		const input = document.createElement('input');
		input.setAttribute('type', 'text');
		input.setAttribute('style', 'width:50px');
		input.setAttribute('id', id + 'newId');
		input.setAttribute('value', originalIdValue);
		tdId.innerHTML = '';
		tdId.appendChild(input);
		const select = document.createElement('select');
		select.setAttribute('style', 'width:50px');
		const optionY = document.createElement('option');
		optionY.setAttribute('value', 'Y');
		const textY = document.createTextNode('Y');
		optionY.appendChild(textY);
		const optionN = document.createElement('option');
		optionN.setAttribute('value', 'N');
		const textN = document.createTextNode('N');
		optionN.appendChild(textN);
		select.appendChild(optionY);
		select.appendChild(optionN);
		if (originalClearanceValue == 'Y') {
			optionY.setAttribute('selected', 'selected');
		} else {
			optionN.setAttribute('selected', 'selected');
		}
		select.setAttribute('id', id + 'newClearance');
		tdClearance.innerHTML = '';
		tdClearance.appendChild(select);
		tdButtons.innerHTML = '';
		const btnSave = document.createElement('span');
		btnSave.setAttribute('class', 'btn');
		btnSave.setAttribute('style', 'background-color:#009900;color:#ffffff');
		btnSave.setAttribute('onclick', 'save(this)');
		const textSave = document.createTextNode('Save');
		btnSave.appendChild(textSave);
		btnSave.setAttribute('id', id + 'save');
		const btnCancel = document.createElement('span');
		btnCancel.setAttribute('class', 'btn');
		btnCancel.setAttribute('style', 'background-color:#000000;color:#ffffff');
		btnCancel.setAttribute('onclick', 'cancel(this)');
		const textCancel = document.createTextNode('Cancel');
		btnCancel.appendChild(textCancel);
		btnCancel.setAttribute('id', id + 'cancel');
		const separate = document.createTextNode(' | ');
		tdButtons.appendChild(btnSave);
		tdButtons.appendChild(separate);
		tdButtons.appendChild(btnCancel);
	}
}
