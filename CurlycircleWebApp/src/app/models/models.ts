export interface EntityCreatedViewModel {
    id: number;
}

export interface LoginDto {
    username: string;
    password: string;
}

export interface TokenViewModel {
    accessToken: string;
}

export interface UserViewModel {
    id: number;
    username: string;
    role: Role;
    token: TokenViewModel;
}

export enum Role {
    User = "User",
    Admin = "Admin",
}

export interface OrderItemsViewModel {
    orderItems: OrderItemViewModel[];
}

export interface OrderItemUpsertDto {
    orderId: number;
    productId: number;
    price: number;
    quantity: number;
}

export interface OrderItem {
    orderId: number;
    productId: number;
    product: ProductViewModel;
    price: number;
    quantity: number;
}

export interface OrderItemViewModel {
    id: number;
    orderId: number;
    productId: number;
    product: ProductViewModel;
    price: number;
    quantity: number;
}

export interface OrdersViewModel {
    orders: OrderViewModel[];
}

export interface OrderUpsertDto {
    orderDateTime: Date;
    orderItems: OrderItemUpsertDto[];
    name: string;
    email: string;
    city: string;
    zipCode: number;
    address: string;
    total: number;
    shippingMethod: ShippingMethod;
    paymentMethod: PaymentMethod;
    phoneNumber: string;
    note: string;
}

export interface OrderViewModel {
    id: number;
    orderDateTime: Date;
    orderItems: OrderItemViewModel[];
    name: string;
    email: string;
    city: string;
    zipCode: number;
    address: string;
    total: number;
    shippingMethod: ShippingMethod;
    paymentMethod: PaymentMethod;
    phoneNumber: string;
    note: string;
}

export enum PaymentMethod {
    MoneyTransfer = "MoneyTransfer",
    CashOnDelivery = "CashOnDelivery",
}

export interface ProductCategoriesViewModel {
    productCategories: ProductCategoryViewModel[];
}

export interface ProductCategoryUpsertDto {
    name: string;
    description: string;
    productIds: number[];
}

export interface ProductCategoryViewModel {
    id: number;
    name: string;
    description: string;
}

export interface ProductsViewModel {
    products: ProductViewModel[];
}

export interface ProductUpsertDto {
    price: number;
    name: string;
    productCategoryId: number;
    description: string;
    imageUrl: string;
    color: string;
    pattern: string;
    material: string;
}

export interface ProductViewModel {
    id: number;
    price: number;
    name: string;
    productCategoryId: number;
    description: string;
    imageUrl: string;
    color: string;
    pattern: string;
    material: string;
}

export enum ShippingMethod {
    Foxpost = "Foxpost",
    MagyarPostaPont = "MagyarPostaPont",
    MagyarPostaCsomagPont = "MagyarPostaCsomagPont",
    HomeDelivery = "HomeDelivery",
    PersonalDelivery = "PersonalDelivery",
}