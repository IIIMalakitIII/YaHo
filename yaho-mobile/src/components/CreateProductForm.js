import React from 'react';
import {View, StyleSheet, Alert, ScrollView} from 'react-native';
import config from '../../config/default'
import { Button, Input } from 'react-native-elements';
import AsyncStorage from "@react-native-community/async-storage";



export default function CreateProductForm(props) {

    const [state, setState] = React.useState({
        OrderId: props.products.orderId,
        Price: 400,
        Tax: 20,
        Description: 'fgrgftgrggfr',
        Link: 'https;//amazon.com',
        ProductName: 'Imma',
    });


    async function  createProduct(){

        try{

            const token = await AsyncStorage.getItem('jwt');
            if(token) {

                const url = config.url + '/api/Products';

                const formData = new FormData();

                formData.append('OrderId', state.OrderId);
                formData.append('Price', state.Price);
                formData.append('Tax', state.Tax);
                formData.append('Description', state.Description);
                formData.append('Link', state.Link);
                formData.append('ProductName', state.ProductName);


                const response = await fetch(url, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'multipart/form-data',
                        'Authorization': `Bearer ${token}`
                    },
                    body: formData
                });

                if (response.ok) {

                    const result = await response.json();
                    setState(result);

                }
            }

        }catch (e) {
            console.log(e);
        }
    }




    return (
        <ScrollView >
            <View style={styles.main}>
                <View style={styles.container}/>
                <View style={styles.container}>

                    <View style={styles.inputBlock}>


                        <Input
                            name = 'ProductName'
                            placeholder = 'product name'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                ProductName: text
                            })}
                            value={state.ProductName}
                        />

                        <Input
                            name = 'Link'
                            placeholder = 'link'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                Link: text
                            })}
                            value={state.Link}
                        />
                        <Input
                            name = 'Description'
                            placeholder = 'description'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                Description: text
                            })}
                            value={state.Description}
                        />
                        <Input
                            name = 'Price'
                            placeholder = 'price'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                Price: Number(text)
                            })}
                            value={state.Price.toString()}
                        />
                        <Input
                            name = 'Tax'
                            placeholder = 'tax'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                Tax: Number(text)
                            })}
                            value={state.Tax.toString()}
                        />

                    </View>

                    <View style={styles.buttonBlock}>
                        <View style={styles.button}>
                            <Button

                                type="solid"
                                title='Add product'
                                onPress={() => {
                                    createProduct();

                                    props.setProducts({
                                        ...props.products,
                                        products: [...props.products.products, {
                                            orderId: state.OrderId,
                                            price: state.Price,
                                            tax: state.Tax,
                                            description:state.Description,
                                            link:state.Link,
                                            productName:state.ProductName,
                                            picture: state.Picture
                                        } ],
                                        opened: false
                                    })

                                    setState({
                                        OrderId: 0,
                                        Price: 0,
                                        Tax: 0,
                                        Description: '',
                                        Link: '',
                                        ProductName: '',
                                        Picture: []
                                    });

                                }}
                            />
                        </View>

                        <View style={styles.button}>
                            <Button
                                type="outline"
                                title="Close"
                                onPress={() => {

                                    setState({
                                        OrderId: 0,
                                        Price: 0,
                                        Tax: 0,
                                        Description: '',
                                        Link: '',
                                        ProductName: '',
                                        Picture: []
                                    });
                                    props.setProducts({
                                        ...props.products,
                                        opened: false
                                    })
                                }}
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
        flexDirection: 'column'
    },
    container: {
        height:220,
        alignItems: 'center',
        flexDirection: 'column',
        justifyContent: 'center',
    },
    buttonBlock: {
        marginTop: 40,
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
});