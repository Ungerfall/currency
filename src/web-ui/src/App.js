import React, { Component } from 'react';
import { Routes, HashRouter } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Routes>
          {AppRoutes.map((route, index) => {
            const { element, ...rest } = route;
            return <HashRouter key={index} {...rest} element={element} />;
          })}
        </Routes>
      </Layout>
    );
  }
}
