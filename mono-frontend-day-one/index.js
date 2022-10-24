function setAction() {
  const formData = new FormData(document.querySelector("form"));
  const formObject = {};
  formData.forEach((value, key) => {
    return (formObject[key] = value);
  });

  console.log(formObject);
  const name = Date.now().toString();
  localStorage.setItem(
    name,
    JSON.stringify({
      firstName: formObject["firstName"],
      lastName: formObject["lastName"],
      email: formObject["email"],
    })
  );
  parent.document.getElementById("form").reset();
  document.getElementById("list").innerHTML = "";
  loadStorage();
}
function loadStorage() {
  Object.keys(localStorage).forEach(function (key) {
    const storageObject = JSON.parse(localStorage.getItem(key));
    const li = document.createElement("li");
    const newUl = document.createElement("ul");
    const fnLi = document.createElement("li");
    fnLi.appendChild(document.createTextNode(storageObject.firstName));
    newUl.appendChild(fnLi);
    const lnLi = document.createElement("li");
    lnLi.appendChild(document.createTextNode(storageObject.lastName));
    newUl.appendChild(lnLi);
    const eLi = document.createElement("li");
    eLi.appendChild(document.createTextNode(storageObject.email));
    newUl.appendChild(eLi);
    const btnLi = document.createElement("li");
    const btn = document.createElement("button");
    btn.onclick = () => {
      removeEmployee(key);
    };
    btn.innerHTML = "Remove";
    btnLi.appendChild(btn);
    newUl.appendChild(btnLi);
    li.appendChild(newUl);
    document.getElementById("list").appendChild(li);
  });
}
function removeEmployee(id) {
  localStorage.removeItem(id);
  document.getElementById("list").innerHTML = "";
  loadStorage();
}
loadStorage();
