import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import Home from "./src/components/Home";
import Profile from "./src/components/Profile";
import SignInForm from "./src/components/SignInForm";
import SignUpForm from "./src/components/SignUpForm";



const Stack = createStackNavigator();


export default function App() {

    return (
        <NavigationContainer>
            <Stack.Navigator initialRouteName="SignInForm">
                <Stack.Screen name="Sign In" component={SignInForm} />
                <Stack.Screen name="Sign Up" component={SignUpForm} />
                <Stack.Screen name="Profile" component={Profile} />
                <Stack.Screen name="Home" component={Home} />
            </Stack.Navigator>
        </NavigationContainer>
    );

}


