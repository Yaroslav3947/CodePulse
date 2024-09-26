import { Category } from "../../category/models/category.model";

export interface Product {
    id: string;
    name: string;
    stock: number
    price: number;
    description: string;
    featuredImageUrl: string;
    urlHandle: string;
    publishedDate: Date;
    categories: Category[];
}