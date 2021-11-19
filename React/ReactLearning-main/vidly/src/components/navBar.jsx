import React, { Component } from 'react'
import { Link, NavLink } from 'react-router-dom';

class NavBar extends Component {
    constructor(props) {
        super(props);
        this.state = {  }
    }
    render() { 
        return (
            <nav className="navbar navbar-expand-lg navbar-light bg-light navbar-center" >
                <Link className="navbar-brand" to="/">Vidly</Link>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <div className="navbar-nav">
                        <NavLink className="nav-link" to="/movies">Movies <span className="sr-only">(current)</span></NavLink>
                        <NavLink className="nav-link" to="/customers">Customers</NavLink>
                        <NavLink className="nav-link" to="/rentals">Rentals</NavLink>
                        <NavLink className="nav-link" to="/login">Login</NavLink>
                        <NavLink className="nav-link" to="/Register">Register</NavLink>
                    </div>
                </div>
            </nav>
         );
    }
}
 
export default NavBar;