import { Outlet, NavLink } from "react-router-dom";
import "./Layout.css";

const Layout = () => (
  <div className="Layout">
    <nav>
    <ul className="link-list">
        <li><NavLink exact to="/">Home</NavLink></li>
        <li><NavLink to="/mybooks">My Books</NavLink></li>
        <li><NavLink to="/addbook">Add Book</NavLink></li>
        <li className="right-align"><NavLink to="/profile">Profile</NavLink></li>
        <li><NavLink to="/SignOut">Sign Out</NavLink></li>
      </ul>
    </nav>
    <Outlet />
  </div>
);

export default Layout;
