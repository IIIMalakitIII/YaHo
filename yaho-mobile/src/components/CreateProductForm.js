import React from 'react';
import {View, StyleSheet, Alert, ScrollView, Image} from 'react-native';
import config from '../../config/default'
import { Button, Input } from 'react-native-elements';
import AsyncStorage from "@react-native-community/async-storage";
import * as ImagePicker from "expo-image-picker";
import Swiper from 'react-native-swiper'



export default function CreateProductForm(props) {

    const [state, setState] = React.useState({
        orderId: props.products.orderId,
        price: 400,
        tax: 20,
        description: 'fgrgftgrggfr',
        link: 'https;//amazon.com',
        productName: 'Imma',
        picture: []
    });

    const pickImage = async () => {
        try {

            let result = await ImagePicker.launchImageLibraryAsync({
                mediaTypes: ImagePicker.MediaTypeOptions.All,
                allowsEditing: true,
                aspect: [4, 3],
                quality: 1,
            });
            if (!result.cancelled) {

                setState({
                    ...state,
                    Picture: state.picture.push(result)
                });
            }
            console.log(result);

        } catch (e) {
            console.log(e);
        }
    };


    const createProduct = async () => {
        try{

            const token = await AsyncStorage.getItem('jwt');
            if(token) {

                const url = config.url + '/api/Products';

                const formData = new FormData();


                formData.append('orderId', state.orderId);
                formData.append('price', state.price);
                formData.append('tax', state.tax);
                formData.append('description', state.description);
                formData.append('link', state.link);
                formData.append('productName', state.productName);


                state.picture.map((value, index) => {

                    let fileName = value.uri.split('/')[value.uri.split('/').length-1];
                    formData.append(
                        'picture',
                        {
                            uri: value.uri,
                            name: fileName,
                            type: `${value.type}/${fileName.split('.')[fileName.split('.').length-1]}`
                        });
                });

               /* state.picture.forEach(x => {
                    formData.append('picture', { uri: x.uri, name:'889b16ed-a2c0-43b2-a8a6-1c70bf858d7c.png', type: 'image/png'});
                })*/


                const response = await fetch(url, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'multipart/form-data',
                        'Authorization': `Bearer ${token}`
                    },
                    body: formData
                });

               /* if (response.ok) {
                    const result = await response.json();
                }*/
            }

        }catch (e) {
            console.log(e);
        }
    }


    console.log("pictures",state.picture);

    return (
        <ScrollView >
            <View style={styles.main}>

                <View style={styles.container}>
                    <View style={styles.inputBlock}>
                        <Input
                            name = 'ProductName'
                            placeholder = 'product name'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                productName: text
                            })}
                            value={state.productName}
                        />
                        <Input
                            name = 'Link'
                            placeholder = 'link'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                link: text
                            })}
                            value={state.link}
                        />
                        <Input
                            name = 'Description'
                            placeholder = 'description'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                description: text
                            })}
                            value={state.description}
                        />
                        <Input
                            name = 'Price'
                            placeholder = 'price'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                price: Number(text)
                            })}
                            value={state.price.toString()}
                        />
                        <Input
                            name = 'Tax'
                            placeholder = 'tax'
                            style={styles.form}
                            onChangeText={text => setState({
                                ...state,
                                tax: Number(text)
                            })}
                            value={state.tax.toString()}
                        />
                    </View>
                    <View style={styles.buttonBlock}>
                        <View style={styles.button}>
                            <Button
                                type="solid"
                                title='Add product'
                                onPress={async () => {
                                    await createProduct();
                                    await props.getProducts(false);
                                }}
                            />
                        </View>

                        <View style={styles.button}>
                            <Button
                                type="outline"
                                title="Close"
                                onPress={async () => {
                                    await props.getProducts(false);
                                }}
                            />
                        </View>
                    </View>
                </View>

                <View  style={styles.imageBlock}>
                    <View style={styles.buttonImage}>
                        <Button
                            title='Add image'
                            onPress={pickImage}
                        />
                    </View>

                    {
                        state.picture.length !== 0 ?

                            <View style={styles.imgContainer}>
                                <Swiper style={styles.wrapper} autoplay={true}  height={300}>
                                    {
                                        state.picture.map((value, index) => {
                                            return (
                                                <View key={index} style={styles.slide}>
                                                    {
                                                        value && <Image source={{uri: value.uri}} style={styles.image}/>
                                                    }
                                                </View>
                                            )
                                        })
                                    }
                                </Swiper>
                            </View>
                            : <View/>

                    }
                </View>


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
        marginTop: 100,
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
    buttonImage:{
        marginTop: 20,
        width: 320,
    },
    imgContainer:{

        marginTop: 40,
        flex: 1,
        marginBottom: 40,
    },
    imageBlock:{

        justifyContent: 'center',
        alignItems: 'center',
    },
    slide: {
        justifyContent: 'center',
        alignItems: 'center',
        flex: 1
    },
    image:{
        width: 300,
        height: 300,
    },
    wrapper: {

    },
});