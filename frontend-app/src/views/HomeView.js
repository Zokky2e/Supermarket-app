import Header from "../components/models/header/Header";
import React, { useEffect } from "react";
import Form from "../components/models/form/Form";
import EmployeeList from "../components/models/employee/EmployeeList";
import "./HomeView.css";

function HomeView() {
  return (
    <>
      <Header />
      <div className="container">
        <Form />
        <EmployeeList />
      </div>
    </>
  );
}

export default HomeView;
