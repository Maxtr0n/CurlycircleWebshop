export interface OrderItemProduct {
    id: number;
    orderId: number;
    quantity: number;
    productId: number;
    price: number;
    name: string;
    productCategoryId: number;
    description: string;
    imageUrl: string;
    color: string;
    pattern: string;
    material: string;
}