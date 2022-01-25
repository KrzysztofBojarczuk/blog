import React from 'react';
import {Navbar, Nav, Col, Container} from 'react-bootstrap';
import {Routes, Route,Button, Link, BrowserRouter} from 'react-router-dom';
import BlogOutput from './BlogOutput';
import Admin from './Admin';


const NavbarBlog = () => {


    return (

        <Container fluid>
            <Navbar bg="light" variant="light" className="justify-content-center">
                <Nav>
                    <Nav.Link href="/">
                        <h5>
                            Strefa czytelnika
          
                        </h5>
                    </Nav.Link>
                    <Nav.Link href="Admin">
                        <h5>
                        Strefa Admina
                        </h5>
                    </Nav.Link>
                </Nav>
            </Navbar>
 
            <Routes>
                <Route path='/' element={< BlogOutput />}/>
                <Route path='Admin' element={< Admin />}/>
            </Routes>
          
        </Container>
    );
}

export default NavbarBlog;
