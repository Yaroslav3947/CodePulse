export interface AddProduct {
    name: string;
    stock: number;
    price: number;
    description: string;
    featuredImageUrl: string;
    urlHandle: string;
    publishedDate: Date;
    categories: string[];
}