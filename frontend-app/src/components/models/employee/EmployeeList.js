/* eslint-disable react-hooks/exhaustive-deps */
import { useEffect } from "react";
import Employee from "./Employee";
import "./EmployeeList.css";
function EmployeeList() {
  useEffect(() => {}, [localStorage]);
  return (
    <div className="container-list">
      <p>Employee List</p>
      <ol id="titles">
        <li>Firstname</li>
        <li>LastName</li>
        <li>Email</li>
        <li>Options</li>
      </ol>
      <ol id="list">
        {Object.keys(localStorage).map((key) => (
          <Employee
            key={key}
            id={key}
            employee={JSON.parse(localStorage.getItem(key))}
          />
        ))}
      </ol>
    </div>
  );
}

export default EmployeeList;
