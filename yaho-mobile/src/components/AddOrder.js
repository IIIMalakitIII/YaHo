import React from 'react';
import {View, StyleSheet, Alert, ScrollView} from 'react-native';
import config from '../../config/default'
import { Button, Input, CheckBox } from 'react-native-elements';
import AsyncStorage from "@react-native-community/async-storage";


export default function AddOrder({ navigation }) {

    const [state, setState] = React.useState({

        deliveryСountry: '',
        deliveryCity: '',
        deliveryCharge: 0,
        deliveryAddress: '',
        bargain: true,
        expectedDate: '',
        title: '',
        comment: '',
        deliveryFromСountry: '',
        deliveryFromCity: '',
        expectedDateFault: ''
    });

    const createOrder = async () => {

        try{
            const token = await AsyncStorage.getItem('jwt');

            if(token) {
                const url = config.url + '/api/Orders/create-order';

                const response = await fetch(url, {
                    method: 'POST',
                    mode: 'cors',
                    cache: 'no-cache',
                    credentials: 'same-origin',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                    },
                    redirect: 'follow',
                    referrerPolicy: 'no-referrer',
                    body: JSON.stringify(state)
                });



                if (response.ok) {
                    const result = await response.json();
                }
            }

        }catch (e) {
            console.log(e);
        }

    };

    return (
        <ScrollView >
            <View style={styles.main}>
                <View style={styles.container}/>
                <View style={styles.container}>

                    <View style={styles.inputBlock}>


                        <Input
                            name = 'title'
                            placeholder = 'title'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                title: text
                            })}
                            value={state.title}
                        />

                        <Input
                            name = 'deliveryСountry'
                            placeholder = 'delivery country'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                deliveryСountry: text
                            })}
                            value={state.deliveryСountry}
                        />

                        <Input
                            name = 'deliveryCity'
                            placeholder = 'delivery city'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                deliveryCity: text
                            })}
                            value={state.deliveryCity}
                        />


                        <Input
                            name = 'deliveryCharge'
                            placeholder = 'delivery charge'

                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                deliveryCharge: Number(text)
                            })}
                            value={state.deliveryCharge.toString()}
                        />


                        <Input
                            name = 'deliveryAddress'
                            placeholder = 'delivery address'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                deliveryAddress: text
                            })}
                            value={state.deliveryAddress}
                        />

                        <CheckBox
                            title='bargain'
                            containerStyle={styles.check}
                            checkedColor='#7b8894'
                            uncheckedColor='#7b8894'
                            size={30}
                            fontFamily='System'
                            checked={state.bargain}
                            onPress={ () =>
                                setState({
                                    ...state,
                                    bargain: !state.bargain
                                })

                            }
                        />


                        <Input
                            name = 'expectedDate'
                            placeholder = 'expected date'
                            style={styles.form}
                            type='date'
                            onChangeText={text => setState({
                                ...state,
                                expectedDate: text
                            })}
                            value={state.expectedDate}
                        />


                        <Input
                            name = 'deliveryFromСountry'
                            placeholder = 'delivery from country'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                deliveryFromСountry: text
                            })}
                            value={state.deliveryFromСountry}
                        />

                        <Input
                            name = 'deliveryFromCity'
                            placeholder = 'delivery from city'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                deliveryFromCity: text
                            })}
                            value={state.deliveryFromCity}
                        />


                        <Input
                            name = 'comment'
                            placeholder = 'comment'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                comment: text
                            })}
                            value={state.comment}
                        />


                        <Input
                            name = 'expectedDateFault'
                            placeholder = 'expected date fault'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                expectedDateFault: text
                            })}
                            value={state.expectedDateFault}
                        />



                    </View>

                    <View style={styles.buttonBlock}>
                        <View style={styles.button}>
                            <Button

                                type="solid"
                                title='Create'
                                onPress={ async () => {
                                    await createOrder();
                                }}
                            />
                        </View>

                        <View style={styles.button}>
                            <Button
                                type="outline"
                                title="Back"
                                onPress={() => navigation.goBack()}
                            />
                        </View>
                    </View>
                </View>
                <View style={styles.container}/>
            </View>
        </ScrollView>

    );
}


const styles = StyleSheet.create({
    main: {
        justifyContent: 'center',
        flexDirection: 'column',
        backgroundColor: '#ffffff',

    },
    container: {

        marginTop: 20,
        marginBottom: 20,
        alignItems: 'center',
        flexDirection: 'column',
        justifyContent: 'center',
    },
    buttonBlock: {
        marginTop: 20,
        flexDirection: 'row',
        justifyContent: 'center',
    },
    inputBlock: {

        width:340,
        flexDirection: 'column',
        justifyContent: 'center',

    },
    button:{
        width: 150,
        marginLeft: 10,
        marginRight: 10
    },
    check:{
        backgroundColor: '#ffffff',
        borderColor: '#7b8894',
        marginBottom: 15,
    }

});