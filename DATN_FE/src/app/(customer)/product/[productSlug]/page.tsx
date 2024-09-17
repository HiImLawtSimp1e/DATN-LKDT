import Loading from "@/components/shop/loading";
import ShopProductDetail from "@/components/shop/product-detail/product-detail";
import { Suspense } from "react";

const Product = async ({ productSlug }: { productSlug: string }) => {
  const res = await fetch(`http://localhost:5000/api/Product/${productSlug}`, {
    method: "GET",
    next: { tags: ["shopProductDetail"] },
  });

  const product: ApiResponse<IProduct> = await res.json();
  // console.log(product.data);

  return <ShopProductDetail product={product.data} />;
};

const ProductDetailPage = ({ params }: { params: { productSlug: string } }) => {
  const { productSlug } = params;
  return (
    <Suspense fallback={<Loading />}>
      <Product productSlug={productSlug} />
    </Suspense>
  );
};

export default ProductDetailPage;
