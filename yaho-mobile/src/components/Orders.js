import React , { useState, useEffect }from 'react';
import {StyleSheet, Text, View, ScrollView, SafeAreaView} from 'react-native';
import AsyncStorage from "@react-native-community/async-storage";
import config from "../../config/default";
import { Button, Input} from 'react-native-elements';
import Products from "./Products";
import OrderDetails from "./OrderDetails";


export default function Profile({ navigation }) {

    const [orders, setOrders] = useState([]);
    const [filter, setFilter] = useState('');




    const getOrders = async (flag) => {
        try{
            setProducts({ ...products, create:  flag})
            const token = await AsyncStorage.getItem('jwt');

            if(token) {

                const url = config.url + '/api/Orders/order-list';

                const response = await fetch(url, {
                    method: 'GET',
                    mode: 'cors',
                    cache: 'no-cache',
                    credentials: 'same-origin',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                    },
                    redirect: 'follow',
                    referrerPolicy: 'no-referrer',
                });

                if (response.ok) {

                    const result = await response.json();
                    console.log("load orders");
                    setOrders(result);
                    return result;
                }
            }
        }catch (e) {
            console.log(e);
        }
    };


    useEffect(() => {

        async function fetchData(){
            const data =  await getOrders();
        }
        fetchData();

    },[]);


    const [products, setProducts] = useState({
        open: false,
        create: false,
        products: [],
        orderId: 0
    });


    if(products.open){

        return (
            <OrderDetails products = {products} setProducts = {setProducts} />
        );

    }else{

        return (

            <ScrollView >


                <View style={styles.container}>


                    <View style={styles.inputContainer}>
                        <View style={styles.input}>
                            <Input
                                name = 'filter'
                                placeholder = 'filter'
                                style={styles.form}
                                onChangeText={
                                    text => setFilter(text)
                                }

                                value={filter}
                            />
                        </View>
                        <View style={styles.button}>
                            <Button
                                type="solid"
                                title="update"
                                linearGradientProps={{
                                    colors: ['pink', '#0a7161' ],
                                    start: { x: 0, y: 0.5 },
                                    end: { x: 0, y: 0.5 },
                                }}
                                onPress={ async () => {

                                    await getOrders();
                                }}
                            />
                        </View>
                    </View>


                    {
                        orders.map((value, index) => {

                            if(
                                value.title.toUpperCase().indexOf(filter.toUpperCase()) != -1 ||
                                value.comment.toUpperCase().indexOf(filter.toUpperCase()) != -1 ||
                                value.deliveryFrom.toUpperCase().indexOf(filter.toUpperCase()) != -1 ||
                                value.deliveryPlace.toUpperCase().indexOf(filter.toUpperCase()) != -1 ||
                                value.initialDate.toUpperCase().split('T')[0].indexOf(filter.toUpperCase()) != -1 ||
                                value.expectedDate.toUpperCase().split('T')[0].indexOf(filter.toUpperCase()) != -1
                            ){
                                return (
                                    <View key={index} style={styles.orderBlock}>
                                        <View style={styles.orderContent}>
                                            <Text style={styles.orderTitle}> {value.title}</Text>
                                            <Text> {value.comment}</Text>
                                            <Text> Delivery from: {value.deliveryFrom}</Text>
                                            <Text> Delivery to: {value.deliveryPlace}</Text>
                                            <Text> Initial date: {value.initialDate.split('T')[0]}</Text>
                                            <Text> Expected date: {value.expectedDate.split('T')[0]}</Text>
                                        </View>
                                        <Button
                                            type="outline"
                                            title="Overview"
                                            onPress={() => {
                                                setProducts({
                                                    open: true,
                                                    products: value.products,
                                                    orderId: value.orderId
                                                })
                                            }}
                                        />
                                    </View>
                                )
                            }
                        })
                    }
                </View>
            </ScrollView>
        );
    }
}



const styles = StyleSheet.create({

    container: {
        marginTop: 50,
        marginBottom: 50,
        alignItems: 'center',
        justifyContent: 'center'
    },
    orderBlock:{
        marginTop: 20,
        marginBottom: 20,
    },
    orderTitle:{
        fontWeight:'bold'
    },
    orderContent:{
        marginBottom:10
    },
    button:{

    },
    inputContainer:{
        marginTop: 30,
        marginBottom:30,
        width: 300,

    },
    input:{

    },
});