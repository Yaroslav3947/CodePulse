import { Category } from "../../category/models/category.model";

export interface Cosmetic {
    id: string;
    name: string;
    brand: string;
    price: number;
    description: string;
    featuredImageUrl: string;
    urlHandle: string;
    publishedDate: Date;
    isVisible: boolean;
    categories: Category[];
}