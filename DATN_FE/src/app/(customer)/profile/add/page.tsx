import Loading from "@/components/shop/loading";
import CreateAddressForm from "@/components/shop/profile/create-address-form";
import { Suspense } from "react";

const CreateAddressPage = () => {
  return (
    <Suspense fallback={<Loading />}>
      <CreateAddressForm />
    </Suspense>
  );
};

export default CreateAddressPage;
