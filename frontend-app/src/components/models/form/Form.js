import { useState } from "react";
import Button from "../../shared/button/Button";
import Input from "./Input";
import "./Form.css";
function Form() {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  function submitForm(e) {
    e.preventDefault();
    localStorage.setItem(
      Date.now().toString(),
      JSON.stringify({
        firstName: firstName,
        lastName: lastName,
        email: email,
      })
    );
    setEmail("");
    setFirstName("");
    setLastName("");
  }
  return (
    <div className="container-form">
      <p>Enter new employee</p>
      <form>
        <Input
          label="FirstName"
          type="text"
          value={firstName}
          onChange={(e) => {
            setFirstName(e.target.value);
          }}
        />
        <Input
          label="LastName"
          type="text"
          value={lastName}
          onChange={(e) => {
            setLastName(e.target.value);
          }}
        />
        <Input
          label="Email"
          type="email"
          value={email}
          onChange={(e) => {
            setEmail(e.target.value);
          }}
        />
        <Button
          padding="16px"
          text="Submit"
          onClick={(e) => {
            submitForm(e);
          }}
        />
      </form>
    </div>
  );
}

export default Form;
