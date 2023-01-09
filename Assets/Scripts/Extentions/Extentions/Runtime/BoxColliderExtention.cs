/**
################################################################################
#          File: ColliderExtentions.cs                                         #
#          File Created: Monday, 23rd May 2022 3:19:58 pm                      #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.Extentions.BoxColliderExtention
{
	static class BoxColliderExtention
	{
		/// <summary>
		/// Return all colliders inside a box collider
		/// </summary>
		/// <param name="boxCollider"></param>
		/// <param name="layerMask"></param>
		/// <returns></returns>
		public static Collider[] GetCollidersInside(this BoxCollider boxCollider, int layerMask = Physics.AllLayers)
		{
			Collider[] hit = Physics.OverlapBox(
				boxCollider.transform.position + boxCollider.center,
				Vector3.Scale(boxCollider.bounds.extents, boxCollider.transform.lossyScale),
				boxCollider.transform.rotation,
				layerMask
			);

			return (hit);
		}
	}
}