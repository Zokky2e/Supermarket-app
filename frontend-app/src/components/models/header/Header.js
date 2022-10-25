import logo from "./moje-e.jpg";
import "./Header.css";
function Header() {
  return (
    <div className="navigation-bar">
      <div className="navigation-bar-logo">
        <img src={logo} alt="logo" />
      </div>
      <div className="navigation-bar-title">
        <h1>EmployeesWebDemo</h1>
      </div>
      <div className="navigation-bar-more-info">
        <a
          href="https://github.com/Zokky2e/Supermarket-app"
          target="_blank"
          rel="noreferrer"
        >
          View more
        </a>
      </div>
    </div>
  );
}

export default Header;
