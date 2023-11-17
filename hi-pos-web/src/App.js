import { Route, Routes } from 'react-router-dom';
import Layout from "./components/layout";
import SignUp from "./components/don-vi/signUp.component";
import SignIn from "./components/don-vi/signIn.component";
import ForgotPassword from "./components/don-vi/forgotPassword.component";
import ConfirmIdentity from "./components/don-vi/confirm.component";
import ListDonVi from "./components/don-vi/listdv.component";
import EditDonVi from "./components/don-vi/edit.component";
import './translation/i18n';
import { PATH_NO_LAYOUT } from './consts/constsCommon';
function App() {

  const AppRoutes = [
    {
      path: '/',
      element: <ListDonVi />
    },
    {
      path: 'sign-up',
      element: <SignUp />
    },
    {
      path: 'sign-in',
      element: <SignIn />
    },
    {
      path: 'forgot-password',
      element: <ForgotPassword />
    },
    {
      path: 'confirm',
      element: <ConfirmIdentity />
    },
    {
      path: 'units',
      element: <ListDonVi />
    },
    {
      path: 'edit-unit',
      element: <EditDonVi />
    },
  ];
  return (<Routes>
    {AppRoutes.map((route, index) => {
      const { element, ...rest } = route;
      if (route.path === "/") {
        return (
          <Route key={index} {...rest} element={<Layout>{element}</Layout>} />
        );
      } else {
        if (PATH_NO_LAYOUT.includes(route.path)) {
          return (
            <Route
              key={index}
              {...rest}
              element={element}
            />
          );
        } else {
          return (
            <Route
              key={index}
              {...rest}
              element={<Layout name={element.name}>{element}</Layout>}
            />
          );
        }
      }
    })}
  </Routes>);
}

export default App;