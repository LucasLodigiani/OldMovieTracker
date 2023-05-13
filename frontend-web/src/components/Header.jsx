import React from 'react';

function Header() {
  return (
      <>
          <nav className="navbar navbar-expand-sm bg-dark navbar-dark">
              <div className="container-fluid">
                  <a className="navbar-brand" href="#">Logo</a>
                  <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#collapsibleNavbar">
                      <span className="navbar-toggler-icon"></span>
                  </button>
                  <div className="collapse navbar-collapse" id="collapsibleNavbar">
                      <ul className="navbar-nav">
                          <li className="nav-item">
                              <a className="nav-link" href="#">Home</a>
                          </li>
                          <li className="nav-item">
                              <a className="nav-link" href="#">Movies</a>
                          </li>

                          <li className="nav-item dropdown">
                              <a className="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown">List</a>
                              <ul className="dropdown-menu">
                                  <li><a className="dropdown-item" href="#">Link</a></li>
                                  <li><a className="dropdown-item" href="#">Another link</a></li>
                                  <li><a className="dropdown-item" href="#">A third link</a></li>
                              </ul>
                          </li>
                      </ul>
                  </div>
              </div>
          </nav>
      </>
  );
}

export default Header;