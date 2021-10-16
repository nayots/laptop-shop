import { ProductType } from '../models/product-type';

export const ConfigurationConstants = {
  Token_Key: "LaptopShop_Token",
  Endpoints: {
    auth: "auth",
    cart: "cart",
    products: "products",
  },
  DemoCredentials: {
    username: "admin",
    password: "admin",
  },
  DebounceTimes: {
    Normal: 300,
    VerySlow: 1500,
  },
  Images: {
    noImage:
      "https://res.cloudinary.com/fehbot/image/upload/v1634385931/laptop-shop/noimage.png",
    cartTile:
      "https://res.cloudinary.com/fehbot/image/upload/v1634335563/laptop-shop/laptop.png",
    [ProductType.Unknown]:
      "https://res.cloudinary.com/fehbot/image/upload/v1634385931/laptop-shop/noimage.png",
    [ProductType.Storage]:
      "https://res.cloudinary.com/fehbot/image/upload/v1634386370/laptop-shop/hdd.png",
    [ProductType.Ram]:
      "https://res.cloudinary.com/fehbot/image/upload/v1634386370/laptop-shop/ram-memory.png",
    [ProductType.Color]:
      "https://res.cloudinary.com/fehbot/image/upload/v1634386370/laptop-shop/color-circle.png",
    [ProductType.Laptop]:
      "https://res.cloudinary.com/fehbot/image/upload/v1634386370/laptop-shop/laptop-product.png",
  },
};
