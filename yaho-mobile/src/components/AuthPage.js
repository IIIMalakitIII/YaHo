import React from 'react';
import { View, Button, AsyncStorage} from 'react-native';
import SingUpForm from './SignUpForm';
import SingInForm from './SignInForm';
import UserPage from './UserPage';



export default function SignInForm() {

    const [state, setState] = React.useState({
        signUp: false,
        signIn: true
    });


    async function changePage(){


        if(state.signIn === true){
            setState({
                signUp: true,
                signIn: false
            })
        }else{
            setState({
                signUp: false,
                signIn: true
            })
        }

    }


    async function removeItemValue(key) {
        try {
            await AsyncStorage.removeItem(key);
            return true;
        }
        catch(exception) {
            return false;
        }
    }


    const token =  AsyncStorage.getItem('jwt');

    if (token !== null) {

        return (
            <View>
                <UserPage/>
                <Button
                    title='LOG OUT'

                    onPress={ async () => {
                        removeItemValue('jwt')

                    }}
                />
            </View>
        );

    }


    if(state.signIn === true){
        return (
            <View>
                <SingInForm/>
                <Button
                    title={'Sign Up'}
                    color='#341bff'
                    onPress={changePage}
                />
            </View>
        );
    }else{

        return (
            <View>
                <SingUpForm/>
                <Button
                    title={'Sign In'}
                    color='#341bff'
                    onPress={changePage}
                />
            </View>
        );
    }
}

