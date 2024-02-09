import { Category } from "../../category/models/category.model";
import { BlogPostComment as BlogPostComment } from "../../public/models/add-comment.model";

export interface BlogPost {
    id: string;
    title: string;
    shortDescription: string;
    content: string;
    featuredImageUrl: string;
    urlHandle: string;
    author: string;
    publishedDate: Date;
    isVisible: boolean;
    categories: Category[];
    likes: string[];
    comments: BlogPostComment[];
}