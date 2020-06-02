import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import {createDrawerNavigator} from "@react-navigation/drawer";
import SignInForm from "./src/components/SignInForm";
import SignUpForm from "./src/components/SignUpForm";
import SignOut from "./src/components/SignOut";
import Profile from "./src/components/Profile";
import AsyncStorage from "@react-native-community/async-storage";
import UserOrders from "./src/components/UserOrders";
import Orders from "./src/components/Orders";



const Drawer = createDrawerNavigator();

export default function App() {

    const [token, setToken] = React.useState(null);

    const getData = async () => {
        try {
            const value = await AsyncStorage.getItem('jwt');

            if(value !== null) {
               setToken(value);
               return value;
            }
            return null;
        } catch(e) {
            console.log(e);
        }
    };

    const userToken = getData();


    return (
        <NavigationContainer>
            <Drawer.Navigator>
                {
                    token == null ? (
                        <>
                            <Drawer.Screen name="Sign In" component={SignInForm} initialParams={{setToken}}/>
                            <Drawer.Screen name="Sign Up" component={SignUpForm} />
                        </>
                    ) : (
                        <>
                            <Drawer.Screen name="Profile" component={Profile} />
                            <Drawer.Screen name="Find order" component={Orders} />
                            <Drawer.Screen name="My orders" component={UserOrders} />
                            <Drawer.Screen name="Sign Out" component={SignOut} initialParams={{setToken}} />
                        </>
                    )
                }
            </Drawer.Navigator>
        </NavigationContainer>
    );

}

