import Button from "../../shared/button/Button";
import "./Employee.css";
function Employee(props) {
  console.log(props.id);
  function removeEmployee(id) {
    localStorage.removeItem(id);
  }
  return (
    <li key={props.key}>
      <ul>
        <li>{props.employee.firstName}</li>
        <li>{props.employee.lastName}</li>
        <li>{props.employee.email}</li>

        <li>
          <Button
            padding="8px"
            text="Remove"
            onClick={() => {
              removeEmployee(props.id);
            }}
          />
        </li>
      </ul>
    </li>
  );
}

export default Employee;
