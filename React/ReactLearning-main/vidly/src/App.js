import React, { Component } from 'react';
import { Route, Redirect, Switch } from 'react-router-dom';
import Movies from './components/movies';
import Customers from './components/customers';
import Rentals from './components/rentals';
import NotFound from './components/notFound';
import NavBar from './components/navBar';
import MovieForm from './components/movieForm';
import LoginForm from './components/loginForm';
import './App.css';
import RegisterForm from './components/common/registerForm';

class App extends Component {
  state = {  }
  render() { 
    return (
      <div>
          <NavBar></NavBar>
          <main className="container">
          <Switch>
            <Route path="/Register" component={RegisterForm}></Route>
            <Route path="/login" component={LoginForm}></Route>
            <Route path="/movies/:id" component={MovieForm}></Route>
            <Route path="/movies" component={Movies}></Route>
            <Route path="/customers" component={ Customers}></Route>
            <Route path="/rentals" component={Rentals}></Route>
            <Route path="/not-Found" component={NotFound}></Route>
            <Redirect from="/" exact to="/movies" />
            <Redirect to="/not-Found" />
          </Switch>
          </main>
        </div>
    );
  }
}
 
export default App;