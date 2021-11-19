import React from "react";
import { Link } from "react-router-dom";
import { Navbar, Container } from "react-bootstrap";



function NavBarExt() {  
  return (  
    <Navbar bg="dark" variant="dark"  >
    <Container>
    <Link className="navbar-brand" to="/" size="lg">
        Todo List 
      </Link>
      <img src={"favicon.ico"} text="" ></img>
    </Container>
  </Navbar>
  );  
}  
  
export default NavBarExt; 