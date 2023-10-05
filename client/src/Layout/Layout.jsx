import { Outlet } from "react-router-dom";
import "./Layout.css";
import NavBar from "./NavBar";

const Layout = () => (
  <div className="Layout">
   <NavBar />
    <Outlet />
  </div>
);

export default Layout;
