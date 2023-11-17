import React from "react";
import CategoryIcon from '@mui/icons-material/Category';
import AcUnitIcon from '@mui/icons-material/AcUnit';
import SettingsIcon from '@mui/icons-material/Settings';
import PrivacyTipOutlinedIcon from '@mui/icons-material/PrivacyTipOutlined';
import StorefrontIcon from '@mui/icons-material/Storefront';
import PaidIcon from '@mui/icons-material/Paid';
import PrecisionManufacturingIcon from '@mui/icons-material/PrecisionManufacturing';
import ManageAccountsIcon from '@mui/icons-material/ManageAccounts';
import GroupsRoundedIcon from '@mui/icons-material/GroupsRounded';
import AssessmentIcon from '@mui/icons-material/Assessment';
import DashboardIcon from '@mui/icons-material/Dashboard';
import { colors } from "@mui/material";
import { pink } from "@mui/material/colors";
import VerifiedIcon from '@mui/icons-material/Verified';
// import PsychologyAltIcon from '@mui/icons-material/PsychologyAlt';
const isUserAuth = JSON.parse(localStorage.getItem('user'));

export const menu = [
  {
    icon: <DashboardIcon  color="success"/>,
    title: "Trang Chủ",
    isAuth: isUserAuth,
    to: '/',
    no: 1150,
  },
  {
    icon: <AcUnitIcon color="primary"/>,
    title: "Tài khoản",
    isAuth: isUserAuth,
    no: 1146,
    items: [
      {
        title: "Danh sách đơn vị",
        to: '/units',
        isAuth: isUserAuth,
        no: 46,
      }
    ]
  },
  {
    icon: <SettingsIcon color="primary"/>,
    title: "Thiết lập",
    isAuth: isUserAuth,
    no: 1149,
    items: [
      {
        title: "- Thông tin đơn vị",
        to: '/edit-unit',
        isAuth: isUserAuth,
        no: 44
      },
    ]
  },
  {
    icon: <VerifiedIcon color="success"/>,
    title: "Version: 10230910",
    isAuth: isUserAuth,
  },
];
