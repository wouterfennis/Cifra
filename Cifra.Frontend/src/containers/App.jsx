import React from 'react';
import { Layout, Breadcrumb } from 'antd';
import NavigationBar from '../components/navigation/NavigationBar';
import { useStoreState } from 'easy-peasy';
import './App.scss';

const { Header, Content, Footer } = Layout;

const App = () => {
  const collapsed = useStoreState((state) => state.ui.navigationBar.collapsed);
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <NavigationBar />
      <Layout
        className="site-layout"
        style={{ marginLeft: collapsed ? 80 : 200 }}
      >
        {/* <Header
          className="site-layout-background"
          style={{ position: 'fixed', zIndex: 1, width: '100%' }}
        /> */}
        <Content style={{ margin: '0 16px' }}>
          <Breadcrumb style={{ margin: '16px 0' }}>
            <Breadcrumb.Item>Klas</Breadcrumb.Item>
            <Breadcrumb.Item>3A</Breadcrumb.Item>
          </Breadcrumb>
          <div
            className="site-layout-background"
            style={{ padding: 24, minHeight: '100%' }}
          >
            TODO
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
            <br />
            ...
          </div>
        </Content>
        <Footer style={{ textAlign: 'center' }}>
          {`Cifra © 2020 - ${new Date().getFullYear()} Created by Wouter F. &
          Lucien K.`}
        </Footer>
      </Layout>
    </Layout>
  );
};

export default App;
