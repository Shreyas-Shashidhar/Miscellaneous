import React from "react";
import NavbarExt from "./components/NavbarExt";
import Main from "./pages/Main";
import { BrowserRouter, Route } from "react-router-dom";
import "./App.css";

function App() {  
  return (  
    <BrowserRouter>
    <NavbarExt />
    <Route exact path="/" component={Main} />
    </BrowserRouter>
  );  
}  
  
export default App;  