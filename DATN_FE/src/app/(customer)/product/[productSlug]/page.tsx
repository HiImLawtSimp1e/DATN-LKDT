import Loading from "@/components/shop/loading";
import ShopProductDetail from "@/components/shop/product-detail/product-detail";
import { Metadata } from "next";
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

export async function generateMetadata({
  params,
}: {
  params: { productSlug: string };
}): Promise<Metadata> {
  const res = await fetch(
    `http://localhost:5000/api/Product/${params.productSlug}`,
    {
      method: "GET",
      next: { tags: ["shopProductDetail"] },
    }
  );

  const product: ApiResponse<IProduct> = await res.json();

  return {
    title: product?.data?.seoTitle || "FStore - Linh kiện điện tử chính hãng",
    description:
      product?.data?.seoDescription || "FStore - Linh kiện điện tử chính hãng",
    keywords:
      product?.data?.seoKeyworks || "FStore - Linh kiện điện tử chính hãng",
  };
}

const ProductDetailPage = ({ params }: { params: { productSlug: string } }) => {
  const { productSlug } = params;
  return (
    <Suspense fallback={<Loading />}>
      <Product productSlug={productSlug} />
    </Suspense>
  );
};

export default ProductDetailPage;
